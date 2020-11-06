const http = require('http');
const {
  uniqueNamesGenerator,
  adjectives,
  colors,
  animals,
  names,
  starWars,
} = require('unique-names-generator');

getAPIHostAndPort = function (req, appendEndSlash) {
  return (
    (req.connection && req.connection.encrypted ? 'https' : 'http') +
    '://' +
    req.headers.host +
    (appendEndSlash ? '/' : '')
  );
};

http
  .createServer(function (req, res) {
    const { rawHeaders, httpVersion, method, socket, url } = req;
    const { remoteAddress, remoteFamily } = socket;
    console.log(
      JSON.stringify({
        timestamp: Date.now(),
        rawHeaders,
        httpVersion,
        method,
        remoteAddress,
        remoteFamily,
        url,
      })
    );
    if (req.url.includes('/title/')) {
      const url = new URL(req.url, getAPIHostAndPort(req, true));

      const seed = req.url.split('/title/')[1];
      const randomTitle = uniqueNamesGenerator({
        dictionaries: [colors, starWars, adjectives, animals, names],
        length: 5,
        style: 'capital',
        separator: ' ',
        seed,
      });
      res.statusCode = 200;
      res.end(
        JSON.stringify({
          title: randomTitle,
        })
      );
    } else {
      res.statusCode = 404;
      res.end('Not Found');
    }
  })
  .listen(8080, '0.0.0.0', () => console.log(`server listening`));
