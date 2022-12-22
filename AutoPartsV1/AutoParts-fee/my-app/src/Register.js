import React, { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/register.css";
import axios from "axios";

export default function Register() {
	const emailInput = useRef();
	const identificationInput = useRef();
	const nameInput = useRef();
	const surnameInput = useRef();
	const passwordInput = useRef();
	const passwordInputTwo = useRef();
	const navigate = useNavigate();

	const [name, setName] = useState([]);
	const [email, setEmail] = useState([]);
	const [password, setPassword] = useState([]);

	const Register = async (e) => {
		e.preventDefault();
		try {
			await axios.post("http://localhost:5106/api/register", {
				userName: name,
				email: email,
				password: password,
			});
			navigate("/login");
		} catch (error) {
			if (error.response) {
				let error = document.getElementById("errorLogin");
				error.style.color = "red";
				error.textContent = "Incorrect username or password";
			}
		}
	};

	function directToMainPage(e) {
		navigate("/");
	}

	function directToLogin(e) {
		navigate("/login");
	}

	return (
		<>
			<div className="registerBody">
				<form className="form-signup-Register center-middle" onSubmit={Register}>
					<h2>Sign up</h2>
					<p>Sign up to create a new AutoParts account</p>
					<br></br>
					<p id="errorLogin"></p>
					<div className="form-group">
						<div className="row">
							<div class="col-md-6">
								<input type="text" className="form-control" name="firstname" placeholder="First Name" required></input>
							</div>
							<br></br>
							<div class="col-md-6">
								<input type="text" className="form-control" name="lastname" placeholder="Last Name" required></input>
							</div>
						</div>
					</div>
					<br></br>
					<div className="form-group">
						<input
							type="email"
							className="form-control"
							name="email"
							placeholder="Email Address"
							required
							value={email}
							onChange={(e) => setEmail(e.target.value)}></input>
					</div>
					<br></br>
					<div className="form-group">
						<input
							type="text"
							className="form-control"
							name="text"
							placeholder="Username"
							required
							value={name}
							onChange={(e) => setName(e.target.value)}></input>
					</div>
					<br></br>
					<div className="form-group">
						<input
							type="password"
							className="form-control"
							name="password"
							placeholder="Password"
							required
							value={password}
							onChange={(e) => setPassword(e.target.value)}></input>
					</div>
					<br></br>
					<div className="form-group">
						<input type="password" className="form-control" name="confirm_password" placeholder="Confirm password" required></input>
					</div>
					<br></br>
					<div className="row">
						<div class="col-md-6">
							<Button className="buttonLog" variant="success btn btn-block submit-button" onClick={Register}>
								Submit
							</Button>
						</div>
						<div class="col-md-6">
							<Button className="buttonLog" variant="outline-success btn btn-block submit-button" onClick={directToMainPage}>
								Cancel
							</Button>
						</div>
					</div>
					<br></br>
					<p>
						Already have an account?
						<Button className="buttonLog" variant="link" onClick={directToLogin}>
							Log in
						</Button>
					</p>
				</form>
				<footer>
					<p>AutoParts @ Copyright, 2022</p>
				</footer>
			</div>
		</>
	);
}