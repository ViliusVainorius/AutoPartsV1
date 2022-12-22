import React, { useEffect, useRef, useState } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { Button, Nav, Navbar, NavDropdown, Form, Container, Card, Modal } from "react-bootstrap";
import ProgressBar from "react-bootstrap/ProgressBar";
import DropdownButton from "react-bootstrap/DropdownButton";
import Dropdown from "react-bootstrap/Dropdown";
import axios from "axios";
import { MDBCard, MDBCardImage, MDBCardBody, MDBCardTitle, MDBCardText, MDBRow, MDBCol, MDBBtn } from "mdb-react-ui-kit";

import "./styles/recipes.css";

export default function Cars() {
	const navigate = useNavigate();

	const [show, setShow] = useState(false);

	const handleClose = (e) => {
		e.preventDefault(e);
		setShow(false);
	};
	const handleShow = (e) => {
		e.preventDefault(e);
		setShow(true);
	};

	const [brandInputs, getBrandData] = useState([]);
	const [brandOutputs, setBrandData] = useState([]);

	const [brandName, setBrandName] = useState([]);
	const [brandDescription, setBrandDescription] = useState([]);
	const [carBrandId, setCarBrandId] = useState([]);

	const brandHandle = (e) => {
		setCarBrandId(e);
		setBrandData(e);
	};
	useEffect(() => {
		// setLoading(false);
		// if(sessionStorage.getItem("token") === null) {
		//     navigate("/");
		// }
		fetchGetAllBrands();
	}, []);

	function create() {
		setShow(false);
		if (brandName.length === 0 || brandDescription.length === 0) {
			let error = document.getElementById("errorCreate");
			error.textContent = "All fields should be filled!";
		} else {
			let json = {};
			json["Name"] = brandName;
            json["Description"] = brandDescription;

			if (JSON.stringify(json) != "{}") {
				json = JSON.stringify(json);
				fetchCreate(json);
			}
			navigate("/cars");
		}
	}

	function fetchCreate(json) {
		const requestOptions = {
			method: "POST",
			headers: {
				Authorization: "Bearer " + localStorage.getItem("token"),
				"Content-Type": "application/json",
			},
			body: json,
		};
		fetch("http://localhost:5106/api/brands", requestOptions).then((res) => afterFetchCreate(res));
	}

	function afterFetchCreate(res) {
		if (res.status != 200) {
			let error = document.getElementById("errorCreate");
			res.text().then((result) => (error.textContent = result));
		} else {
			window.location.reload(true);
		}
	}

	function fetchGetAllBrands() {
		axios.get("http://localhost:5106/api/brands").then((res) => getBrandData(res.data));
	}

	function directToLogin(e) {
		navigate("/login");
	}

	const renderNav = (isAdmin) => {
		if (isAdmin === "admin") {
			return (
				<Navbar collapseOnSelect expand="lg" bg="white" variant="white">
					<Container className="nav-container-background">
						<Navbar.Brand id="foodle-logo" href="#home">
							AutoParts
						</Navbar.Brand>
						<Navbar.Toggle aria-controls="responsive-navbar-nav" />
						<Navbar.Collapse id="responsive-navbar-nav">
							<Nav className="me-auto">
								<Nav.Link href="/cars">Cars</Nav.Link>
								<Nav.Link href="/createCar">Create car</Nav.Link>
								<Nav.Link href="/createBrand">Create brand</Nav.Link>
							</Nav>
							<Nav>
								<Nav.Link href="#">
									<div className="username-text-bold">{isAdmin}</div>
								</Nav.Link>
								<Nav.Link eventKey={2} href="/">
									Logout
								</Nav.Link>
							</Nav>
						</Navbar.Collapse>
					</Container>
				</Navbar>
			);
		} else {
			return (
				<Navbar collapseOnSelect expand="lg" bg="white" variant="white">
					<Container className="nav-container-background">
						<Navbar.Brand id="foodle-logo">AutoParts</Navbar.Brand>
						<Navbar.Toggle aria-controls="responsive-navbar-nav" />
						<Navbar.Collapse id="responsive-navbar-nav">
							<Nav className="me-auto">
								<Nav.Link href="/cars">Cars</Nav.Link>
							</Nav>
							<Nav>
								<Nav.Link>Hi,</Nav.Link>
								<Nav.Link href="#">
									<div className="username-text-bold">{isAdmin}</div>
								</Nav.Link>
								<Nav.Link eventKey={2} href="/">
									Logout
								</Nav.Link>
							</Nav>
						</Navbar.Collapse>
					</Container>
				</Navbar>
			);
		}
	};

	return (
		<>
			{/* --- Navigation --- */}
			<div className="space"></div>
			{renderNav(localStorage.getItem("username"))}

			{/* --- Search Filter --- */}
			<div className="space"></div>
			<div className="hero-image">
				<p className="hero-text">Create new brand</p>
			</div>
			{/* --- Recipes --- */}
			<div className="recipes-container">
				<div className="center-form">
					<form>
						<p id="errorCreate" className="errorTextTitle"></p>
						<br></br>
						<div className="form-outline mb-4">
							<label className="form-label" htmlFor="form4Example1">
								Brand name
							</label>
							<input
								type="text"
								id="form4Example1"
								className="form-control"
								value={brandName}
								onChange={(e) => {
									setBrandName(e.target.value);
								}}
							/>
						</div>

						<div className="form-outline mb-4">
							<label className="form-label" htmlFor="form4Example3">
								Description
							</label>
							<textarea
								className="form-control"
								id="form4Example3"
								rows="4"
								value={brandDescription}
								onChange={(e) => {
									setBrandDescription(e.target.value);
								}}></textarea>
						</div>

						<button type="submit" className="btn btn-primary btn-block mb-4 button-margin-right-30" onClick={handleShow}>
							Create brand
						</button>
					</form>
				</div>
			</div>
			<p>&ensp;</p>
			<p>&ensp;</p>
			<p>&ensp;</p>

			<div className="recipesFooter">
				<p className="footer-text-recipes">
					<br></br>AutoParts @ Copyright, 2022
				</p>
			</div>
			<Modal show={show} onHide={handleClose}>
				<Modal.Header closeButton>
					<Modal.Title>New brand</Modal.Title>
				</Modal.Header>
				<Modal.Body>Are you sure you want to create a new brand?</Modal.Body>
				<Modal.Footer>
					<Button variant="primary" onClick={() => create()}>
						Create
					</Button>
					<Button variant="outline-primary" onClick={handleClose}>
						Close
					</Button>
				</Modal.Footer>
			</Modal>
		</>
	);
}
