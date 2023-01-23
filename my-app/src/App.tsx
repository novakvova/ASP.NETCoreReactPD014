import React, { useEffect } from 'react';
import logo from './logo.svg';
import './App.css';

const App=() => {

  const handleSuccessGoogle = (resp: any) => {
    console.log("Resp google", resp);
    const {credential} = resp;
    console.log("Google token", credential);
    
  }

  useEffect(() => {
    const clientId="1023020461333-q2vicrpm2rnjreik8qcotc3s8e6af59p.apps.googleusercontent.com";
    window.google.accounts!.id.initialize({
      client_id: clientId,
      callback:handleSuccessGoogle 
    });
    window.google.accounts!.id.renderButton(document.getElementById("googleBtn"),
      {theme: "outline", size: "Large"} );
  }, []);
  
  return (
    <>
      <h1>Login google</h1>
      <div id="googleBtn"></div>
    </>
  );
}

export default App;
