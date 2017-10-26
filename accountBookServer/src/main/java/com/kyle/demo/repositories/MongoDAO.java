package com.kyle.demo.repositories;

import com.kyle.demo.models.MongoUser;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.ArrayList;

public interface MongoDAO extends MongoRepository<MongoUser, String>{

	public MongoUser findByUsername(String username);
}
