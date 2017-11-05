package com.kyle.demo.userServices;

import com.kyle.demo.models.Item;

import java.util.List;

public interface ItemService {
    public String createItem(Item item) throws Exception;
    public List<Item> getItemByDate(String username, String date);
    public List<Item> getItemByLocation(String username, String loc);
    public List<Item> showAll(String username);
}
