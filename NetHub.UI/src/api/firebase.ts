import {initializeApp} from "firebase/app";
import {getAuth, GoogleAuthProvider, FacebookAuthProvider} from 'firebase/auth';
const firebaseConfig = {
  apiKey: "AIzaSyDkdue408vWDYaC6xhdOk5GJUeJoubSmuE",
  authDomain: "nethub-356011.firebaseapp.com",
  projectId: "nethub-356011",
  storageBucket: "nethub-356011.appspot.com",
  messagingSenderId: "336236854992",
  appId: "1:336236854992:web:9661da472a0648aae0a8d5",
  measurementId: "G-C4C74BQFC7"
};

const app = initializeApp(firebaseConfig);
export const googleProvider = new GoogleAuthProvider()
export const facebookProvider = new FacebookAuthProvider()
export const auth = getAuth(app)
