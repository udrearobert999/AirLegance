import './Information.module.css'
import * as React from 'react';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';





export default function Information() {
    return (
      <div className="Information">
    <Typography gutterBottom variant="h3" allingn="center">
    Here we have the Information page of AirLegance travel agency

   </Typography>

      
      <Card sx={{ maxWidth: 450, margin:"0 auto", padding:"20px 5px"}}>
      <CardContent>
        <Typography sx={{ fontSize: 20 }} color="black" gutterBottom>
          BAGAGE
          
        </Typography>
        <img src="./src\images\cards\bagage1.jpg"/>
     
        <Typography variant="body2" color="text.secondary">
          Here you can find some usefull 
          <br />
          information about the bagage
        </Typography>
     
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
      </Card>

      {/* <h5>
      <Card sx={{ minWidth: 300}}></Card>
      <CardContent>
        <Typography sx={{ fontSize: 20 }} color="black" gutterBottom>
          {bull}REQUIRED DOCUMENTS
        </Typography> */}
        {/* <img src="./src\images\cards\documents.jpg"/> 
       
{/*        
        <Typography variant="body2" color="text.secondary">
          At this section you can find more about 
          <br />
          all the documents you need to proceed
          <br />
          a reservation then to do the check-in
        </Typography>
     
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
      </h5> */}

      {/* <h6>
      <Card sx={{ minWidth: 300}}></Card>
      <CardContent>
        <Typography sx={{ fontSize: 20 }} color="black" gutterBottom>
          {bull}ASSISTANCE AND HEALTH
        </Typography>
       
       
        <Typography variant="body2" color="text.secondary">
         Here we are providing for our users 
         <br/>
         information regarding health insurances 
         <br/>
         for travelling...
        </Typography>
     
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
      </h6> */} 
    </div>
    );
  }