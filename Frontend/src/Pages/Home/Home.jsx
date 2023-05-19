import * as React from 'react';
import Box from '@mui/material/Box';

import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Autocomplete from '@mui/material/Autocomplete';
import { faPlaneDeparture } from '@fortawesome/free-solid-svg-icons';
import { faPlaneArrival } from '@fortawesome/free-solid-svg-icons';
import { faSuitcaseRolling } from '@fortawesome/free-solid-svg-icons';
import { faCalendarDays } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { DatePicker as MuiDatePicker } from '@mui/lab';

import { useState } from 'react';
import dayjs from 'dayjs';

import { format } from 'date-fns';
import { DayPicker } from 'react-day-picker';

export default function Home() {
  const [tripType, setTripType] = useState('');
  const [departureLocation, setDepartureLocation] = useState('');
  const [arrivalLocation, setArrivalLocation] = useState('');
  const [filteredLocations, setFilteredLocations] = useState(locations);
  const [selectedDate, setSelectedDate] = useState(null);

  const [departureDate, setDepartureDate] = useState(null);
  const [arrivalDate, setArrivalDate] = useState(null);

  //arrival date select
  const handleTripTypeChange = (event, value) => {
    setTripType(value?.value || '');
  };
  //arrival city select
  const handleDepartureLocationChange = (event, value) => {
    setDepartureLocation(value?.label || '');
    setFilteredLocations(
      locations.filter((location) => location.label !== (value?.label || ''))
    );
  };
  const handleArrivalLocationChange = (event, value) => {
    setArrivalLocation(value?.label || '');
    setFilteredLocations(
      locations.filter((location) => location.label !== (value?.label || ''))
    );
  };
  //min departure day today
  const handleDateChange = (date) => {
    setSelectedDate(date);
  };
  //arrival date select
  const handleDepartureDateChange = (newDate) => {
    setDepartureDate(newDate);
  };
  const handleArrivalDateChange = (newDate) => {
    setArrivalDate(newDate);
  };
  const minArrivalDate = departureDate
    ? dayjs(departureDate).add(1, 'day').toDate()
    : null;
  return (
    <>
      <br></br>
      {/* <div style={{backgroundImage: 'url(https://compote.slate.com/images/222e0b84-f164-4fb1-90e7-d20bc27acd8c.jpg)',
              backgroundRepeat: "no-repeat",
              backgroundSize: "cover",
              height:1000,width:900
  }}> */}
      <Box
        display='flex'
        justifyContent='center'
        alignItems='center'
        sx={{
          alignItems: 'center',
          justifyContent: 'center',
          height: '100%',
        }}
        noValidate
        autoComplete='off'
      >
        <FontAwesomeIcon icon={faPlaneDeparture} />
        <p>Departing from</p>
        <Autocomplete
          disablePortal
          id='combo-box-demo'
          options={filteredLocations}
          getOptionLabel={(option) => option.label}
          sx={{ width: 300 }}
          onChange={handleDepartureLocationChange}
          renderInput={(params) => (
            <TextField {...params} label='Please select a departure location' />
          )}
        />
        <FontAwesomeIcon icon={faPlaneArrival} />
        <p>Arriving at</p>
        <Autocomplete
          disablePortal
          id='combo-box-demo'
          options={filteredLocations}
          getOptionLabel={(option) => option.label}
          sx={{ width: 300 }}
          onChange={handleArrivalLocationChange}
          renderInput={(params) => (
            <TextField {...params} label='Please select an arrival location' />
          )}
        />
      </Box>
      <br></br>
      <Box
        display='flex'
        justifyContent='center'
        alignItems='center'
        sx={{
          alignItems: 'center',
          justifyContent: 'center',
          height: '100%',
        }}
        noValidate
        autoComplete='off'
      >
        <FontAwesomeIcon icon={faSuitcaseRolling} />
        <p>Type</p>
        <Autocomplete
          disablePortal
          id='combo-box-demo'
          options={trips}
          sx={{ width: 300 }}
          onChange={handleTripTypeChange}
          renderInput={(params) => (
            <TextField
              {...params}
              label='Please select the type of your trip'
            />
          )}
        />

        <FontAwesomeIcon icon={faCalendarDays} />
        <p>Select your date</p>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DemoContainer components={['DatePicker']}>
            {/* <DatePicker label="Departure date" value={departureDate} onChange={handleDepartureDateChange} /> */}
            <DatePicker
              label='Departure date'
              value={selectedDate}
              onChange={handleDateChange}
              minDate={dayjs()}
            />
          </DemoContainer>
        </LocalizationProvider>

        {!tripType || tripType !== 'oneway' ? (
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DemoContainer components={['DatePicker']}>
              {/* <DatePicker label="Arrival date" value={arrivalDate} onChange={handleArrivalDateChange} minDate={minArrivalDate} disabled={!departureDate}  /> */}
              <DatePicker label='Arrival date' />
            </DemoContainer>
          </LocalizationProvider>
        ) : null}
        {tripType == 'oneway' && (
          <LocalizationProvider dateAdapter={AdapterDayjs}>
            <DemoContainer components={['DatePicker']}>
              <DatePicker label='Arrival date' disabled />
            </DemoContainer>
          </LocalizationProvider>
        )}
      </Box>

      {/* </div> */}
    </>
  );
}
const locations = [
  { label: 'Craiova,Romania' },
  { label: 'Bucharest,Romania' },
  { label: 'Iasi,Romania' },
  { label: 'Cluj,Romania' },
  { label: 'Brasov,Romania' },
  { label: 'Rome,Italy' },
  { label: 'Milano,Italy' },
  { label: 'Paris,France' },
  { label: 'Berlin,Germany' },
  { label: 'Madrid,Spain' },
  { label: 'Barcelona,Spain' },
  { label: 'Budapesta,Hungary' },
];

const trips = [
  { label: 'One-Way Trip', value: 'oneway' },
  { label: 'Round Trip', value: 'roundtrip' },
];
