import './Information.module.css'
import * as React from 'react';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import CardMedia from '@mui/material/CardMedia';





export default function Information() {
    return (
      <div className="Information">
    <Typography gutterBottom variant="h3" allingn="center" sx={{ textAlign: "center", padding:"20px 5px" }}>
    Here we have the Information page of AirLegance travel agency
   </Typography>
   <Typography variant="h6" gutterBottom sx={{ textAlign: "left", padding:"20px 5px" }}>
        Whether you are all set to book your ticket or are looking for practical information after your booking, we've got you covered!
      </Typography>

   <div style={{ display: "flex", justifyContent: "space-between", flexWrap: "wrap" }}>
      <Card sx={{ maxWidth: 400, margin:"0", padding:"20px 5px", display: "block", textAlign: "left" }}>
      <CardContent>
        <Typography sx={{ fontSize: 20,textAlign: "center" }} color="black" gutterBottom allingn="center">
          BAGAGE
          
        </Typography>
        
        <CardMedia
        component="img"
        height="234"
        width="100%"
        image="./src\images\cards\excedent-bagage.jpg" 
        alt="excedent-bagage"
      />
        <Typography variant="body2" color="text.secondary"  sx={{padding:"20px 5px"}}>
          
          All about your luggage, such as what you can and cannot bring and what to do if something goes wrong.
          <br />
          <br />
        </Typography>
     
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
      </Card>

      <Card sx={{ maxWidth: 400, margin:"0", padding:"20px 5px", display: "block", textAlign: "left" }}>
      <CardContent>
        <Typography sx={{ fontSize: 20,textAlign: "center" }} color="black" gutterBottom>
          REQUIRED DOCUMENTS 
          
        </Typography>
        <CardMedia
        component="img"
        height="234"
        width="100%"
        image="./src\images\cards\documents.jpg" 
        alt="documents"
      />
     
     <Typography variant="body2" color="text.secondary" sx={{padding:"20px 5px"}} >
          At this section you can find more about 
          all the documents you need to proceed
          a reservation then to do the check-in.
        </Typography>
     
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
      </Card>

      <Card sx={{ maxWidth: 400, margin:"0", padding:"20px 5px", display: "block", textAlign: "left" }}>
      <CardContent>
        <Typography sx={{ fontSize: 20,textAlign: "center"}} color="black" gutterBottom>
          MANAGING YOUR BOOKING
          
        </Typography>
        <CardMedia
        component="img"
        height="234"
        width="100%"
        image="./src\images\cards\manage-booking.jpg" 
        alt="documents"
      />
     
        <Typography variant="body2" color="text.secondary"sx={{padding:"20px 5px"}}>
        Check how to change, upgrade, or cancel your flight, and how to make a name correction.
        <br />
        <br />
        </Typography>
     
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
      </Card>
      </div>
    
    </div>
    );
  }