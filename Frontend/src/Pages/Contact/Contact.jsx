import * as React from 'react';
import Autocomplete from '@mui/material/Autocomplete';


import {Container, Row, Col} from "react-bootstrap";

import Style from "./Contact.module.css"

export default function Contact() {
  return (
    <>
   <Container>
     <Row className="mb-5 mt-3">
      <Col lg='8'>
        <h1 className="display-4 mb-4">
            Contact me
        </h1>
      </Col>
     </Row>

     <Row className="sec_sp">
      <Col lg='5' className='mb-5'>
          <h3 className="color_sec py-4">Get in touch</h3>
          <address>
            <strong>Email : user@react.com</strong>
            <br/>
            <br/>
            <p>
              <strong> Phone : +40 xxx xxx xxx</strong>
            </p>
          </address>
          <p>Description of this contact page</p>
      </Col>

      <Col lg='7' className='d-flex align-items-center'>
        <form className="contact_form w-100">
          <Row>
            <Col lg='6' className="form-group">
              <input 
                className="form-control"
                id="name"
                name="name"
                placeholder="Name"
                type="text"
              
              />
            </Col>
            <br/>
            
            <Col lg='6' className="form-group">
              <input 
                className="form-control rounded-0"
                id="email"
                name="email"
                placeholder="Email"
                type="email"
              
              />
            </Col>
           
          </Row>
          <br/>
        
            <br/>
          <textarea 
                    className="form-control rounded-0" id="message"
                    name="message"
                    placeholder="Message"
                    rows='5'
          ></textarea>
          <br/>
          <Row>
            <Col lg='12' className="form-group">
            <button  className="btn ac_btn" type="submit">Send</button>
            </Col>
          </Row>
        </form>
      </Col>


     </Row>
     

    </Container> 
    <Autocomplete
              disablePortal
              id="combo-box-demo"
              options={contactoptions}
              sx={{ width: 300 }}
              renderInput={(params) => <TextField {...params} label="contact_message" />}
            /> 
     </>
  );
}

const contactoptions =['complaints','questions','bla','bla'];