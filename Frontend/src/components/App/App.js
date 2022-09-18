import './App.css';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Home from '../Home/Home'
import Navbar from '../Navbar/Navbar';
import Footer from '../Footer/Footer';
import PlantCategories from '../Plants/PlantCategories';
import Plants from '../Plants/Plants';
import LoginForm from '../Auth/LoginForm';
import RegisterForm from '../Auth/RegisterForm';
import Plant from '../Plants/Plant';
import Quiz from '../Quiz/Quiz';
import MyProfile from '../MyProfile/MyProfile';
import UserProvider from '../../providers/userProvider';
import Documentation from '../Documentation/Documentation';
import AboutUs from '../About Us/AboutUs';
import Contact from '../Contact/Contact';

function App() {
  return (
    <Router>
        <UserProvider>
          <main>
            <Navbar />
            <Routes>
              <Route path="/" element={<Navigate replace to="/home"/>} />
              <Route path='/home' element={<Home/>} />
              <Route path='/plant-categories' element={<PlantCategories/>}/>
              <Route path='/plants' element={<Plants/>}/>
              <Route path='/login' element={<LoginForm/>}/>
              <Route path='/register' element={<RegisterForm/>}/>
              <Route path='/plants/:plantId' element={<Plant/>}/>
              <Route path='/mini-quiz/:plantId' element={<Quiz/>}/>
              <Route path='/my-profile' element={<MyProfile/>}/>
              <Route path='/documentation' element={<Documentation/>}/>
              <Route path='/about-us' element={<AboutUs/>}/>
              <Route path='/contact' element={<Contact/>}/>
            </Routes>
            <Footer />
          </main>
        </UserProvider>
    </Router>
  );
}

export default App;
