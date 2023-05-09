import * as React from 'react';
import Box from '@mui/material/Box';

import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Autocomplete from '@mui/material/Autocomplete';
import { faPlaneDeparture } from "@fortawesome/free-solid-svg-icons";
import { faPlaneArrival } from '@fortawesome/free-solid-svg-icons';
import { faSuitcaseRolling } from '@fortawesome/free-solid-svg-icons';
import { faCalendarDays } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';



import { format } from 'date-fns';
import { DayPicker } from 'react-day-picker';



export default function Home() {
  return (

    <>
  <br></br>
  {/* <div style={{backgroundImage: 'url(https://compote.slate.com/images/222e0b84-f164-4fb1-90e7-d20bc27acd8c.jpg)',
              backgroundRepeat: "no-repeat",
              backgroundSize: "cover",
              height:1000,width:900
  }}> */}
 <Box 
      display = "flex"
      justifyContent="center"
      allign-alignItems= "center"
      sx={{
          
          alignItems: "center",
          justifyContent: "center",
          height: "100%"}}

      noValidate
      autoComplete="off"
    >
      <FontAwesomeIcon icon={faPlaneDeparture} />
      <h>Departing from</h>
      <Autocomplete
        disablePortal
        id="combo-box-demo"
        options={locations}
        sx={{ width: 300}}
        renderInput={(params) => <TextField {...params} label="Please select a departure location" />}
      />
      <FontAwesomeIcon icon={faPlaneArrival} />
      <h>Arriving at</h>
      <Autocomplete
        disablePortal
        id="combo-box-demo"
        options={locations}
        sx={{ width: 300}}
        renderInput={(params) => <TextField {...params} label="Please select an arrival location" />}
      />
      
  </Box>
  <br></br>
  <Box display = "flex"
      justifyContent="center"
      allign-alignItems= "center"
      sx={{
          alignItems: "center",
          justifyContent: "center",
          height: "100%"}}

      noValidate
      autoComplete="off"
    >
      <FontAwesomeIcon icon={faSuitcaseRolling} />
      <h>Type</h>
      <Autocomplete
        disablePortal
        id="combo-box-demo"
        options={trips}
        sx={{ width: 300}}
        renderInput={(params) => <TextField {...params} label="Please select the type of your trip" />}
      />
      
      <FontAwesomeIcon icon={faCalendarDays} />
        <h>Select your date</h>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DemoContainer components={['DatePicker']}>
        <DatePicker label="Departure date" />
      </DemoContainer>
    </LocalizationProvider>

          <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DemoContainer components={['DatePicker']}>
          <DatePicker label="Arival date" disabled/>
        </DemoContainer>
      </LocalizationProvider>

  </Box>
 
  {/* </div> */}
  </>
  );
}
const locations = [
  { label: 'Craiova,Romania'},
  { label: 'Bucharest,Romania'},
  { label: 'Iasi,Romania'},
  { label: 'Cluj,Romania' },
  { label: 'Brasov,Romania' },
  { label: "Rome,Italy" },
  { label: 'Milano,Italy'},
  { label: 'Paris,France'},
  { label: 'Berlin,Germany'},
  { label: 'Madrid,Spain'},
  { label: 'Barcelona,Spain'},
  { label: 'Budapesta,Hungary'},
];

const trips = [
  { label: 'One-Way Trip',
    value: 'oneway'   },
  { label: 'Round Trip',
    value: 'roundtrip'},
];