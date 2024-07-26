
import './App.css';
import Navbar from './Components/Navbar/Navbar';
import { Outlet } from 'react-router';
import Hero from './Components/Hero/Hero';
function App() {

  return (
    <>
        <Navbar />
        <Outlet />
    </>
  );

}

export default App;

