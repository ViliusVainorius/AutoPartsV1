import React, { useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import { Button } from "react-bootstrap";

import "./styles/main.css";

export default function Main() {
    const navigate = useNavigate();

    useEffect(() => {
		sessionStorage.clear();
	});

    function directToRegister(e) {
		navigate("/register");
	}

	function directToLogin(e) {
		navigate("/login");
	}

    return(
        <>
            <section id="hero">
				<div className="containerMain">
					<div className="info">
						<h1>AutoParts</h1>
						<h2>Auto parts forum, for your car!</h2>
						<p>
							Everything your car needs, any part, any detail - you can find it here!
						</p>
						<div className="main">
							<Button className="button1" variant="outline-success" onClick={directToLogin}>
								Sign in
							</Button>
						</div>
						<div className="main">
							<Button className="button1" variant="success" onClick={directToRegister}>
								Join now for free
							</Button>
						</div>
					</div>
				</div>
				<footer>
					<p>AutoParts @ Copyright, 2022</p>
				</footer>
			</section>
        </>
    );
}