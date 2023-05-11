import * as React from 'react';
import Autocomplete from '@mui/material/Autocomplete';
import Typography from '@mui/material/Typography';
import Style from "./Contact.module.css"
import { Card } from '@material-ui/core';
import { CardContent } from '@mui/material';
import { Grid } from '@mui/material';
import { TextField } from '@mui/material';
import { Button } from '@mui/material';

export default function Contact() {
  return (
    <div className="Contact">
      <Typography gutterBottom variant="h3" allingn="center" sx={{ textAlign: "center" , padding:"20px 5px"}}>
        AirLegance Contact Page
        </Typography>
        <Card style={{maxWidth: 450, margin:"0 auto", padding:"20px 5px"}}>
          <CardContent>
            <Typography gutterBottom variant="h5">Contact Us</Typography>
            <Typography gutterBottom color="textSecondary" variant="body2" component="p" sx={{ padding:"10px 1px"}}>Fill up the form and our team will get back to you within 24 hours.</Typography>
            <form>
            <Grid container spacing={1}>
              <Grid xs={12} sm={6} item>
                <TextField label="First Name" placeholder="Enter First Name" fullWidth required />
              </Grid>
              <Grid xs={12} sm={6} item>
                <TextField label="Last Name" placeholder="Enter Last Name" fullWidth required />
              </Grid>
              <Grid xs={12} item>
                <TextField type='email' label="Email" placeholder="Enter email" fullWidth required />
              </Grid>
              <Grid xs={12} item>
                <TextField type='number' label="Phone" placeholder="Enter phone number" fullWidth required />
              </Grid>
              <Grid xs={12} item>
                <TextField label="Message" multiline rows={4} placeholder="Type your message here" fullWidth required />
              </Grid>
              <Grid xs={12} item>
                <Button type="submit" variant="contained" color="primary" fullWidth>Submit</Button>
              </Grid>
            </Grid>
            </form>
          </CardContent>
        </Card>
      
    </div>
  );
}

