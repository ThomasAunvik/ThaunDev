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
      <Grid container spacing={4}>
        <Grid item>
          <Formik
            enableReinitialize
            initialValues={user}
            onSubmit={async (editedUser) => {
              const updatedUser = await editUser(user.id, editedUser);

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
                <div className={styles.inputSpacing}>
                  <InputLabel>Email</InputLabel>
                  <Input
                    name="email"
                    type="email"
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.email}
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
        <Grid item>
          <Avatar
            alt={user.firstName + " " + user.lastName}
            src={user.profilePicture?.data}
            className={styles.largeProfilePicture}
          />
          <div>
            <Button
              className={styles.changeProfilePictureButton}
              variant="outlined"
              onClick={() => setUploadOpen(true)}
            >
              Upload
            </Button>
            <p className={styles.smallUploadText}>Allowed: png/jpeg/gif</p>
            <p className={styles.smallUploadText}>Max: 20MB</p>
          </div>
          <DropzoneDialog
            open={uploadOpen}
            onSave={(files) => onProfileImageUpload(files)}
            acceptedFiles={["image/jpeg", "image/png"]}
            showPreviews={true}
            onClose={() => setUploadOpen(false)}
            filesLimit={1}
            maxFileSize={20000000}
            showAlerts={false}
          />
        </Grid>
      </Grid>
    </div>
  );
};

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    smallUploadText: {
      fontSize: "12px",
      marginTop: "0",
      marginBottom: "0",
    },
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
