import { gql } from "@apollo/client";

export const queryCurrentUser = gql`
  query GetCurrentUser {
    current {
      id
      username
      lastName
      firstName
    }
  }
`;

export const queryGetUser = gql`
  query GetUser($id: Int) {
    user(id: $id) {
      id
      username
      lastName
      firstName
    }
  }
`;
