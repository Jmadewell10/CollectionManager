const express = require('express');
const router = express.Router();
const { getHello } = require('../controllers/apiController');

// Define your API routes
router.get('/hello', getHello);

module.exports = router;
