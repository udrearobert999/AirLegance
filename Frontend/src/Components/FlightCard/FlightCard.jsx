import {
  Card,
  CardContent,
  Typography,
  Grid,
  Box,
  Rating,
} from '@mui/material';

const FlightCard = ({ flight }) => {
  return (
    <Card sx={{ width: '80%', margin: 'auto', marginTop: 2, marginBottom: 2 }}>
      <CardContent>
        <Grid container justifyContent='space-between'>
          <Grid item xs={6}>
            <Typography variant='h6'>{flight.source}</Typography>
            <Typography variant='h6'>{flight.destination}</Typography>
          </Grid>
          <Grid item xs={6}>
            <Box display='flex' flexDirection='column' alignItems='flex-end'>
              <Typography variant='h5' component='div'>
                ${flight.price.toFixed(2)}
              </Typography>
              <Rating name='read-only' value={flight.rating} readOnly />
            </Box>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
};

export default FlightCard;
