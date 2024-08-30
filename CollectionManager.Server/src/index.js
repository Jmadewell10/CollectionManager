require('dotenv').config();
const express = require('express');
const app = express();
const port = process.env.PORT || 3000;
const apiRoutes = require('./routes/apiRoutes');

// Middleware to parse JSON bodies
app.use(express.json());

// Use API routes
app.use('/api', apiRoutes);

// Start the server
app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
