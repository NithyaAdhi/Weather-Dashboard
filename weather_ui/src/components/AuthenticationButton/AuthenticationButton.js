import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

const AuthenticationButton = () => {
  const { isAuthenticated, loginWithRedirect, logout } = useAuth0();

  const buttonStyles = {
    padding: "10px 20px",
    fontSize: "1rem",
    cursor: "pointer",
    border: "none",
    borderRadius: "5px",
    backgroundColor: "#287ff1ff",
    color: "white",
  };

  return isAuthenticated ? (
    <button
      style={buttonStyles}
      onClick={() =>
        logout({ logoutParams: { returnTo: window.location.origin } })
      }
    >
      Log Out
    </button>
  ) : (
    <button style={buttonStyles} onClick={() => loginWithRedirect()}>
      Log In
    </button>
  );
};

export default AuthenticationButton;
