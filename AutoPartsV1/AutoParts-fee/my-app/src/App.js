import Main from "./Main";
import Login from "./Login";
import Register from "./Register";
import Cars from "./Cars";
import CreateCar from "./CreateCar";
import CreateBrand from "./CreateBrand";
import { Route, Routes } from "react-router-dom";


import logo from './logo.svg';
import './App.css';

function App() {
	return (
		<>
			<Routes>
				<Route path="" element={<Main />} />
				<Route path="/login" element={<Login />} />
				<Route path="/register" element={<Register />} />
        		<Route path="/cars" element={<Cars />} />
				<Route path="/createCar" element={<CreateCar />} />
				<Route path="/createBrand" element={<CreateBrand />} />
			</Routes>
		</>
	);
}

export default App;
