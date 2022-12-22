import { createPath, Navigate, useNavigate } from "react-router-dom";
import React, { useEffect, useRef, useState } from "react";
import DropdownButton from "react-bootstrap/DropdownButton";
import Dropdown from "react-bootstrap/Dropdown";
import { Button, Nav, Navbar, NavDropdown, Form, Container, Card } from "react-bootstrap";
import axios from "axios";
import { MDBCard, MDBCardImage, MDBCardBody, MDBCardTitle, MDBCardFooter, MDBCardText, MDBRow, MDBCol, MDBBtn } from "mdb-react-ui-kit";


import "./styles/cars.css";

export default function Recipes() {
    const navigate = useNavigate();

    const [brandInputs, getBrandData] = useState([]);
	const [brandOutputs, setBrandData] = useState([]);
    const [carsInfo, getCarsData] = useState([]);

    const brandHandle = (e) => {
		setBrandData(e);
	};

	useEffect(() => {
		// setLoading(false);
		// if(sessionStorage.getItem("token") === null) {
		//     navigate("/");
		// }
		fetchGetAllCBrands();
		fetchGetAllCars();
	}, []);

    function directToRegister(e) {
		navigate("/register");
	}

	function directToLogin(e) {
		navigate("/login");
	}

    function fetchGetAllCBrands() {
		axios.get("http://localhost:5106/api/brands").then((res) => getBrandData(res.data));
	}

	function fetchGetAllCars() {
		axios.get("http://localhost:5106/api/brands/2/cars").then((res) => getCarsData(res.data));
	}

    function formatDate(date) {
		const event = new Date(date);
		return event.toDateString();
	}


    
    const renderCard = (card) => {
		return (
			<MDBCol>
				<MDBCard className="h-100">
					<MDBCardImage className="card-max-height" src={card.imageURL} alt="..." position="top" />
					<MDBCardBody className="card-body">
						<MDBCardTitle>{card.name}</MDBCardTitle>
						<MDBCardText>{"Engine size: " + card.engineSize}</MDBCardText>
						<MDBCardText>{"Fuel type: " + card.fuelType}</MDBCardText>
						<MDBCardText>{"Year: " + card.creationYear}</MDBCardText>
					</MDBCardBody>
					<MDBCardFooter  href="#" class="btn btn-primary">Get parts </MDBCardFooter>
				</MDBCard>
			</MDBCol>
		);
	};

    const renderNav = (isAdmin) => {
		if (isAdmin === "admin") {
			return (
				<Navbar collapseOnSelect expand="lg" bg="white" variant="white">
					<Container className="nav-container-background">
						<Navbar.Brand id="autoParts-logo" href="#home">
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
						<Navbar.Brand id="autoParts-logo">AutoParts</Navbar.Brand>
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

    return(
        <div className="whole">
			<div className="space"></div> 
			{renderNav(localStorage.getItem("username"))}
            <div className="hero-image">
				<div className="on-image">
					<div className="wrapper">
						<div className="wrapper-element">
							<label className="filter-label">Search car brands</label>
							<input type="search" className="form-control rounded" placeholder="Search" aria-label="Search" />
						</div>
						<div className="wrapper-element"></div>
						<div className="wrapper-element">
							<label className="filter-label">Car brands</label>
							<DropdownButton
								id="btn btn-success dropdown-toggle"
								placeholder="Brand"
								title={"Brand list"}
								onSelect={brandHandle}>
								{brandInputs.map((items) => {
									return <Dropdown.Item eventKey={items.name}>{items.name}</Dropdown.Item>;
								})}
							</DropdownButton>
						</div>
						<div className="wrapper-element">
							<label className="filter-label">
								<span>&#8203;</span>
							</label>
							<br></br>
							<button type="button" className="btn btn-outline-success">
								Search car
							</button>
						</div>
					</div>
				</div>
			</div>
            {/* --- Cars --- */}
			<div className="carsInfo">
				<MDBRow className="row-cols-1 row-cols-md-3 g-5">
					{carsInfo.map((items) => {
						return renderCard(items);
					})}
				</MDBRow>
			</div>
			<div className="carsFooter">
				<p className="footer-text-cars">
					<br></br>AutoParts @ Copyright, 2022
				</p>
                </div>
            
        </div >
    );
}