import { Grid } from "@material-ui/core";
import {
  Avatar,
  Button,
  ButtonGroup,
  createStyles,
  Input,
  InputLabel,
  makeStyles,
  Theme,
} from "@material-ui/core";
import { Form, Formik } from "formik";
import { DropzoneDialog } from "material-ui-dropzone";
import React, { useEffect, useState } from "react";
import { useRecoilState } from "recoil";
import {
  editProfilePicture,
  editUser,
  fetchCurrentUser,
} from "../api/users/actions";
import { IUser } from "../models/user";
import { current } from "../states/users";
import NotLoggedInContainer from "./NotLoggedInContainer";

const ProfileContainer = () => {
  const styles = useStyles();

  const [user, setUser] = useRecoilState(current);

  const [uploadOpen, setUploadOpen] = useState<boolean>(false);

  useEffect(() => {
    fetchCurrentUser(true).then((current) => {
      if (current === null) return;

      setUser(current);
    });
  }, [setUser]);

  if (user === null) return <NotLoggedInContainer />;

  const onProfileImageUpload = async (files: File[]) => {
    setUploadOpen(false);
    if (files.length <= 0) return;

    const file = files[0];
    const arrBuff = await file.arrayBuffer();
    const fileText = btoa(
      String.fromCharCode.apply(null, Array.from(new Uint8Array(arrBuff)))
    );

    const newPic = await editProfilePicture(user.id, fileText);
    if (newPic === null) return;

    setUser({
      ...user,
      profilePicture: newPic,
    });
  };

  return (
    <div>
      <h1>Profile</h1>
      <Grid container spacing={2}>
        <Grid item>
          <Avatar
            alt={user.firstName + " " + user.lastName}
            src={user.profilePicture?.data}
            className={styles.largeProfilePicture}
          />

          <Button
            className={styles.changeProfilePictureButton}
            variant="outlined"
            onClick={() => setUploadOpen(true)}
          >
            Upload
          </Button>
          <DropzoneDialog
            open={uploadOpen}
            onSave={(files) => onProfileImageUpload(files)}
            acceptedFiles={["image/jpeg", "image/png"]}
            showPreviews={true}
            onClose={() => setUploadOpen(false)}
            filesLimit={1}
            showAlerts={false}
          />
        </Grid>
        <Grid item xs={6}>
          <Formik
            enableReinitialize
            initialValues={user}
            onSubmit={async (editedUser, formikhelper) => {
              const compare1 = new Map(Object.entries(editedUser));
              const compare2 = new Map(Object.entries(user));

              const changedValues = new Map<string, any>();
              Array.from(compare1.keys()).forEach((x) => {
                if (compare1.get(x) !== compare2.get(x))
                  changedValues.set(x, compare1.get(x));
              });

              const updatedUser = await editUser(user.id, {
                ...(Object.fromEntries(changedValues) as IUser),
                id: user.id,
              });

              if (updatedUser) {
                setUser(updatedUser);
              }
            }}
          >
            {({
              values,
              errors,
              touched,
              handleChange,
              handleBlur,
              handleSubmit,
              resetForm,
              isSubmitting,
              dirty,
            }) => (
              <Form onSubmit={handleSubmit}>
                <div className={styles.inputSpacing}>
                  <InputLabel>Username</InputLabel>
                  <Input
                    name="username"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.username}
                  />
                </div>
                <div className={styles.inputSpacing}>
                  <InputLabel>First Name</InputLabel>
                  <Input
                    name="firstName"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.firstName}
                  />
                </div>
                <div className={styles.inputSpacing}>
                  <InputLabel>Last Name</InputLabel>
                  <Input
                    name="lastName"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.lastName}
                  />
                </div>
                <div>
                  <ButtonGroup>
                    <Button
                      type="submit"
                      color="default"
                      variant="contained"
                      disabled={isSubmitting || !dirty}
                    >
                      Save
                    </Button>
                    <Button
                      color="default"
                      variant="outlined"
                      disabled={isSubmitting || !dirty}
                      onClick={() => resetForm()}
                    >
                      Cancel
                    </Button>
                  </ButtonGroup>
                </div>
              </Form>
            )}
          </Formik>
        </Grid>
      </Grid>
    </div>
  );
};

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    columnSpacing: {
      marginBottom: theme.spacing(2),
    },
    profilePictureDiv: {
      marginRight: theme.spacing(2),
    },
    changeProfilePictureButton: {
      marginTop: theme.spacing(2),
    },
    largeProfilePicture: {
      width: theme.spacing(20),
      height: theme.spacing(20),
    },
    inputSpacing: {
      marginBottom: theme.spacing(2),
    },
  })
);

export default ProfileContainer;
