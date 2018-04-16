"""Module for base IQ Option base websocket chanel."""

import logging, coloredlogs

class Base(object):
    """Class for base IQ Option websocket chanel."""
    # pylint: disable=too-few-public-methods

    def __init__(self, api):
        """
        :param api: The instance of :class:`IQOptionAPI
            <iqoptionapi.api.IQOptionAPI>`.
        """
        self.api = api

    def send_websocket_request(self, name, msg):
        """Send request to IQ Option server websocket.

        :param str name: The websocket chanel name.
        :param dict msg: The websocket chanel msg.

        :returns: The instance of :class:`requests.Response`.
        """

        logger = logging.getLogger(__name__)

        # By default the install() function installs a handler on the root logger,
        # this means that log messages from your code and log messages from the
        # libraries that you use will all show up on the terminal.
        coloredlogs.install(level='DEBUG')

        # If you don't want to see log messages from libraries, you can pass a
        # specific logger object to the install() function. In this case only log
        # messages originating from that logger will show up on the terminal.
        coloredlogs.install(level='DEBUG', logger=logger)
        coloredlogs.install(fmt='%(asctime)s,%(msecs)03d [%(process)d] %(levelname)s %(message)s')

        # Some examples.
        logger.info({"name": name,
                     "msg": msg})


        return self.api.send_websocket_request(name, msg)
