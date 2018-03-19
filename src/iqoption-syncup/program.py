#from iqoption_api.api import IQOption
from iqoptionapi.api import IQOptionAPI
from iqoption_api.api import IQOption

import logging, coloredlogs
import coloredlogs, logging

import datetime
import time

# Create a logger object.
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
logger.debug("this is a debugging message")
logger.info("this is an informational message")
logger.warning("this is a warning message")
logger.error("this is an error message")
logger.critical("this is a critical message")

#api = IQOption("mongkon.eiadon@hotmail.com","Code11054")
# api.login() # Returns True if successful else False
# api.start_socket_connection()

username = "mongkon.eiadon@gmail.com"
password = "Code11054"



api2 = IQOptionAPI('iqoption.com', username, password)
api2.connect()

api2.getcandles(1, 60, 25)



#The first one is the money you want to set
#2nd is the asset. Check constants.ASSETS for the values
#3rd is the which mode. Turbo is for binary. Don't ask me why
#4th is either call or put

