package com.kyle.demo.userServices;

import java.util.List;

import com.kyle.demo.models.MongoUser;
import com.kyle.demo.repositories.MongoDAO;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service("LRImpl")
public class UserServiceImpl implements UserServices {

    @Autowired
    private MongoDAO mongoDB;

    private static final Logger logger = LoggerFactory.getLogger(UserServiceImpl.class);


    // Method returns a status of a registration operation in String
    // Method accepts two parameters: username and password to register into DB
    @Override
    public String createUser(String username, String password)  {

        if (mongoDB.findByUsername(username) == null) {
            try {
            	// if user is new, save to database
                mongoDB.save(new MongoUser(username, password));
                //mongoDB.save(new MongoUser(username, password));
            } catch (Exception exc) {

            	// Need to handle other exceptions here, like invalid entry to DB,
            	// preferably on a Clients before sending Request
                logger.error("@@@ User failed to be saved... Reason: " + exc.getMessage() + " @@@");
            }
            
            logger.error("@@@ User: " + username + " is registered! @@@");
            return "registered";
        } else {
        	logger.error("@@@ User: " + username + " Already Exists @@@");
            return "duplicate_user";
        }
    } // end of createUser()

    @Override
    public String loginUser(String username, String password) {

    	// Check if user exist in DB. Delegate this task to SqlDAO repository.
    	// Hibernate provides a method findBy___(param) to perform querying.
    	// If you want, you can easily switch to MongoDB
        if (mongoDB.findByUsername(username) == null) {
        	logger.info("@@@ Wrong Username! @@@");
            return "no_user";
        } else {
        	// If user exist in DB, Hibernate will return a POJO of SqlUser. 
        	// We can access it's password field via getter and compare with
        	// password sent in the request from a Client.
            if (!password.equals(mongoDB.findByUsername(username).getPassword())) {
            	logger.info("@@@ Wrong Password! @@@");
                return "wrong_pass";
            } else {
            	// if username/password pass validation, return status back to Client
            	logger.info("@@@ Logged In! @@@");
                return "logged";
            }
        }
    } // end of loginUser this method can be refactored to return a Authentication Token
    	// returned token than can be placed into response header and sent to Client.


    @Override
    public List<Object> showAll() {
        return null;
    }

}
