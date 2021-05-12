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
      authorization: jsonStorage?.access_token
        ? `Bearer ${jsonStorage?.access_token}`
        : "",
    },
  }));

  return forward(operation);
}) as RequestHandler);

export const client = new ApolloClient({
  link: from([authMiddleware, httpLink]),
  cache: new InMemoryCache(),
});

export const fetchObj = async <Type>(query: DocumentNode) => {
  const result = await client.query({
    query: query,
  });

  if (result.error || result.errors) {
    console.error(result.error ?? result.errors);
    return null;
  }

  return result.data as Type;
};
