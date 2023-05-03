import * as React from 'react';
import Box from '@mui/material/Box';

import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Autocomplete from '@mui/material/Autocomplete';
import { faPlaneDeparture } from "@fortawesome/free-solid-svg-icons";
import { faPlaneArrival } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
export default function Home() {
  return (
    
    <>
    <h2>
 <Box
      component="form"

      sx={{ alignItems: "center"}}

      noValidate
      autoComplete="off"
    >
      
      <FontAwesomeIcon icon={faPlaneDeparture} />
      
      <Autocomplete
        disablePortal
        id="combo-box-demo"
        options={locations}
        sx={{ width: 250 ,alignItems: "center"}}
        renderInput={(params) => <TextField {...params} label="Departure" />}
        
      />
      
      <FontAwesomeIcon icon={faPlaneArrival} />
      <Autocomplete
        disablePortal
        id="combo-box-demo"
        options={locations}
        sx={{ width: 250,alignItems: "center" }}
        renderInput={(params) => <TextField {...params} label="Arrival" />}
        
      />
  </Box>
  </h2>
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



