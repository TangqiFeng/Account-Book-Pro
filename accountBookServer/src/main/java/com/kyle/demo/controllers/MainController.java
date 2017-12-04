package com.kyle.demo.controllers;

import com.kyle.demo.models.Item;
import com.kyle.demo.repositories.ItemDAO;
import com.kyle.demo.userServices.ItemService;
import com.kyle.demo.userServices.UserServices;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@RestController
public class MainController {

	// Spring Boot injecting beans through @Autowired annotation
	@Autowired
	@Qualifier("LRImpl")
	private UserServices userService;

	@Autowired
	@Qualifier("ItemImpl")
	private ItemService itemService;

	@Autowired
	private ItemDAO itemDao;

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

	@PostMapping("/additem")
	public String createItem(@RequestBody Item item) throws Exception {
		return itemService.createItem(item);
	}

	@GetMapping("/getitembydate")
	public List<Item> getItemByDate(@RequestParam("month") String month,
									@RequestParam("year") String year,
									@RequestParam("username") String username){
		String date="";
		if(month.equals("0")){
			date = new String(year+"$");
		}else {
			date = new String(month+"/"+year+"$");
		}

		return itemService.getItemByDate(username,date);
	}

	@GetMapping("/getitembyloc")
	public List<Item> getItemByLocation(@RequestParam("location") String loc,
										@RequestParam("username") String username){
		return itemService.getItemByLocation(username,loc);
	}

	@GetMapping("/getallitem")
	public List<Item> getAll(@RequestParam("username") String username){
		return itemService.showAll(username);
	}

	@GetMapping("/deleteitem")
	public String delete(@RequestParam("username") String username,
							 @RequestParam("detail") String detail){
		List<Item> items = itemService.findByUsernameAndDetail(username, detail);
		for (Item item : items){
			itemDao.delete(item);
		}
		return "deleted";
	}


} // end of class
