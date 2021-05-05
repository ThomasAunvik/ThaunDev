import { ApolloClient, DocumentNode, InMemoryCache } from "@apollo/client";

export const client = new ApolloClient({
  uri: process.env.REACT_APP_API,
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
