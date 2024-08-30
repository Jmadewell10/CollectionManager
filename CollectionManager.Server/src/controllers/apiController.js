const getHello = (req, res) => {
    res.json({ message: 'Hello from the API!' });
  };
  
  module.exports = {
    getHello,
  };
  