import { useEffect, useRef } from 'react';

import Style from './Home.module.css';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { Carousel } from 'react-responsive-carousel';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { DatePicker } from '@mui/x-date-pickers';

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

const Home = () => {
  const departureDateRef = useRef(null);
  const returnDateRef = useRef(null);
  const originRef = useRef(null);
  const destinationRef = useRef(null);

  const handleSearch = (event) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    const searchData = {
      origin: formData.get('origin'),
      destination: formData.get('destination'),
      departureDate: formData.get('departureDate'),
      returnDate: formData.get('returnDate'),
    };
    console.log(searchData);
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
                  ref={originRef}
                  sx={{ width: '15rem' }}
                  id='origin'
                  options={locations}
                  getOptionLabel={(option) => option.label}
                  renderInput={(params) => (
                    <TextField {...params} label='Origin' fullWidth />
                  )}
                />
                <Autocomplete
                  ref={destinationRef}
                  sx={{ width: '15rem' }}
                  id='destination'
                  options={locations}
                  getOptionLabel={(option) => option.label}
                  renderInput={(params) => (
                    <TextField {...params} label='Destination' fullWidth />
                  )}
                />
              </Box>
              <Box sx={{ display: 'flex', gap: 0.5, flexWrap: 'wrap' }}>
                <DatePicker
                  ref={departureDateRef}
                  sx={{ width: '15rem' }}
                  label='Departure date'
                  renderInput={(params) => <TextField {...params} />}
                />
                <DatePicker
                  ref={returnDateRef}
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
    </>
  );
};

export default Home;
