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



api = IQOptionAPI("iqoption.com", username, password)
api.connect()

time.sleep(1)

balance = api.profile.balance
time.sleep(1)
logger.info (balance)

api.setactives([1, 2])
time.sleep(1)

api.subscribe('tradersPulse')
reslt = api.buy(1, 76, "turbo", "call")
time.sleep(2)
logger.info(reslt) 

# api = IQOption(username,password)
# api.login()

# api.open_position(direction="call",
#                     expiration_time=api.binary_expiration_list["EURUSD"][-1]["time"],
#                     market_name="EURUSD",
#                     price=5,
#                     type="turbo")



#The first one is the money you want to set
#2nd is the asset. Check constants.ASSETS for the values
#3rd is the which mode. Turbo is for binary. Don't ask me why
#4th is either call or put

