package com.kyle.demo.controllers;

import com.kyle.demo.userServices.UserServices;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;


@RestController
public class MainController {

	// Spring Boot injecting beans through @Autowired annotation
	@Autowired
	@Qualifier("LRImpl")
	private UserServices userService;

	// Server console logger
	private static Logger logger = LoggerFactory.getLogger(MainController.class);

	// handler for sign-up root, excepts parameters from URI and stores as a
	// java variable
	@PostMapping("/sign-up")
	public String signUp(@RequestParam("username") String username, 
						 @RequestParam("password") String password) throws Throwable {
		logger.info("Before Creating User: " + username + "  " + password);
		return userService.createUser(username,password);

	} // end of signUp end-point

	/*
	 * Login end point. Requests parameters from URI, i.e.,
	 * ../login?username=admin&password=123
	 */

	@GetMapping("/login")
	public String login(@RequestParam("username") String username, @RequestParam("password") String password) {
		logger.info("Before Login User: " + username + "  " + password);

		return userService.loginUser(username,password);
	} // end of /login end point


} // end of class