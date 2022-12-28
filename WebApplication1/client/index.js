const express = require("express");
const https = require("https");
const fs = require("fs");

const app = express();

app.get("/", (req, res) => {
    res.sendFile(__dirname + "/frontend.html");
});


const options = {
    key: fs.readFileSync('key.pem'),
    cert: fs.readFileSync('cert.pem')
};
const port = 9999;
const host = "localhost.test";
https.createServer(options, app).listen(port, host, () => {
    console.log(`Application started and Listening on https://${host}:${port}`);
});