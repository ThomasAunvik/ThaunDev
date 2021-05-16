import {
  ApolloClient,
  ApolloLink,
  createHttpLink,
  DocumentNode,
  from,
  InMemoryCache,
  RequestHandler,
} from "@apollo/client";

const httpLink = createHttpLink({
  uri: process.env.REACT_APP_API,
});

const authMiddleware = new ApolloLink(((operation, forward) => {
  const authStorage = localStorage.getItem(
    "oidc.user:" + process.env.REACT_APP_AUTH + ":thaun-dev-web"
  );
  if (!authStorage) return forward(operation);

  const jsonStorage = JSON.parse(authStorage);

  operation.setContext(({ headers = {} }) => ({
    headers: {
      ...headers,
      Authorization: jsonStorage?.access_token
        ? `Bearer ${jsonStorage?.access_token}`
        : "",
    },
  }));

  return forward(operation);
}) as RequestHandler);

const cleanTypeName = new ApolloLink((operation, forward) => {
  if (operation.variables) {
    const omitTypename = (key: string, value: any) =>
      key === "__typename" ? undefined : value;
    operation.variables = JSON.parse(
      JSON.stringify(operation.variables),
      omitTypename
    );
  }
  return forward(operation).map((data) => {
    return data;
  });
});

export const client = new ApolloClient({
  link: from([cleanTypeName, authMiddleware, httpLink]),
  cache: new InMemoryCache(),
});

export const fetchObj = <Type>(
  query: DocumentNode,
  field: string,
  network?: boolean
): Promise<Type | null> => {
  return new Promise<Type | null>((success) => {
    client
      .query({
        query: query,
        fetchPolicy: network ? "network-only" : "cache-first",
      })
      .then((result) => {
        if (result.error || result.errors) {
          console.error(result.error ?? result.errors);
          success(null);
        }

        success(result.data[field] as Type);
      })
      .catch(() => {
        console.log("Error fetching...");
        success(null);
      });
  });
};

export const editObj = <Type>(
  query: DocumentNode,
  field: string,
  variables: Map<string, any>
): Promise<Type | null> => {
  return new Promise<Type | null>((success) => {
    client
      .mutate({
        mutation: query,
        variables: Object.fromEntries(variables),
      })
      .then((result) => {
        if (result.errors) {
          console.error(result.errors);
          success(null);
        }

        success(result.data[field] as Type);
      })
      .catch((error) => {
        console.error("Error fetching...", error);
        success(null);
      });
  });
};
