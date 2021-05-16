import { gql } from "@apollo/client";

export const queryCurrentUser = gql`
  query GetCurrentUser {
    current {
      id
      username
      lastName
      firstName
      email
      profilePicture {
        name
        data
      }
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
      email
      profilePicture {
        name
        data
      }
    }
  }
`;

export const mutationEditUser = gql`
  mutation EditUser($id: Int, $user: GraphEditUserInputType) {
    edituser(id: $id, user: $user) {
      id
      username
      lastName
      firstName
      email
      profilePicture {
        name
        data
      }
    }
  }
`;

export const mutationChangeProfilePicture = gql`
  mutation ChangeProfilePicture($id: Int, $image: String) {
    editprofilepicture(id: $id, image: $image) {
      name
      data
    }
  }
`;
