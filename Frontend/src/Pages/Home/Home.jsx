import { useEffect, useRef, useState } from 'react';

import useAxiosWithErrorRedirect from 'Hooks/useAxiosWithErrorRedirect';

import Style from './Home.module.css';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { Carousel } from 'react-responsive-carousel';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import { Typography } from '@mui/material';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { DatePicker } from '@mui/x-date-pickers';

import FlightCard from 'Components/FlightCard';

const FlightSearchResults = ({ flights }) => {
  if (flights.length === 0) {
    return (
      <Typography variant='h6'>
        No flights found. Please try a different search.
      </Typography>
    );
  }

  return (
    <div>
      {flights.map((flight) => (
        <FlightCard key={flight.id} flight={flight} />
      ))}
    </div>
  );
};

const Home = () => {
  const departureDateRef = useRef(null);
  const returnDateRef = useRef(null);
  const [origin, setOrigin] = useState(null);
  const [destination, setDestination] = useState(null);
  const [flights, setFlights] = useState([]);

  const axios = useAxiosWithErrorRedirect();

  const [locations, setLocations] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('/locations');
        setLocations(response.data.data);
      } catch (err) {
        console.error(err);
      }
    };

    fetchData();
  }, []);

  const handleOriginChange = (event, value) => {
    setOrigin(value);
  };

  const handleDestinationChange = (event, value) => {
    setDestination(value);
  };

  const handleSearch = async (event) => {
    event.preventDefault();
    const searchData = {
      sourceLocationId: origin?.id,
      destinationLocationId: destination?.id,
    };
    try {
      const response = await axios.post('/flights/search-flight', searchData);
      setFlights(response.data.data);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <>
      <Carousel
        className={Style.carousel}
        autoPlay
        infiniteLoop
        useKeyboardArrows
        showStatus={false}
        showThumbs={false}
      >
        <div className={Style.imageContainer}>
          <img src='/carousel-pic1.jpg' alt='carousel-image' />
        </div>
        <div className={Style.imageContainer}>
          <img src='/carousel-pic2.avif' alt='carousel-image' />
        </div>
        <div className={Style.imageContainer}>
          <img src='/carousel-pic3.jpg' alt='carousel-image' />
        </div>
      </Carousel>
      <Card className={Style.card}>
        <CardContent>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <Box
              component='form'
              onSubmit={handleSearch}
              sx={{
                display: 'flex',
                justifyContent: 'space-between',
                flexWrap: 'wrap',
              }}
            >
              <Box sx={{ display: 'flex', gap: 0.5, flexWrap: 'wrap' }}>
                <Autocomplete
                  sx={{ width: '15rem' }}
                  id='origin'
                  options={locations}
                  getOptionLabel={(option) =>
                    `${option.city} (${option.country})`
                  }
                  value={origin}
                  onChange={handleOriginChange}
                  renderInput={(params) => (
                    <TextField {...params} label='Origin' fullWidth />
                  )}
                />
                <Autocomplete
                  sx={{ width: '15rem' }}
                  id='destination'
                  options={locations}
                  getOptionLabel={(option) =>
                    `${option.city} (${option.country})`
                  }
                  value={destination}
                  onChange={handleDestinationChange}
                  renderInput={(params) => (
                    <TextField {...params} label='Destination' fullWidth />
                  )}
                />
              </Box>
              <Box sx={{ display: 'flex', gap: 0.5, flexWrap: 'wrap' }}>
                <DatePicker
                  inputRef={departureDateRef}
                  sx={{ width: '15rem' }}
                  label='Departure date'
                  renderInput={(params) => <TextField {...params} />}
                />
                <DatePicker
                  inputRef={returnDateRef}
                  sx={{ width: '15rem' }}
                  label='Return date'
                  renderInput={(params) => <TextField {...params} />}
                />
              </Box>
              <Button
                type='submit'
                variant='contained'
                className={Style.buttonAccent}
              >
                Search
              </Button>
            </Box>
          </LocalizationProvider>
        </CardContent>
      </Card>
      <FlightSearchResults flights={flights} />
    </>
  );
};

export default Home;
