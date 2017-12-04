package com.kyle.demo.repositories;

import com.kyle.demo.models.Item;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;

import java.util.ArrayList;

public interface ItemDAO extends MongoRepository<Item, String> {
    public ArrayList<Item> findByUsername(String username);

    @Query("{'$and' : [{'username' : ?0},{'date' : { $regex: ?1 }}]}")
    public ArrayList<Item> findByUsernameAndDate(String username, String date);

    @Query("{'$and' : [{'username' : ?0},{'location' : ?1}]}")
    public ArrayList<Item> findByUsernameAndLocation(String username, String loc);

    @Query("{'$and' : [{'username' : ?0},{'detail' : ?1}]}")
    public ArrayList<Item> findByUsernameAndDetail(String username, String detail);

}
