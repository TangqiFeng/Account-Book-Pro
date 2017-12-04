package com.kyle.demo.userServices;

import com.kyle.demo.models.Item;
import com.kyle.demo.repositories.ItemDAO;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service("ItemImpl")
public class ItemServiceImpl implements ItemService {

    @Autowired
    ItemDAO itemdao;

    public String createItem(Item item) throws Exception {
        itemdao.save(item);
        return "success";
    }

    public List<Item> getItemByDate(String username, String date) {
        List<Item> items = itemdao.findByUsernameAndDate(username, date);
        return items;
    }

    public List<Item> getItemByLocation(String username, String loc) {
        List<Item> items = itemdao.findByUsernameAndLocation(username, loc);
        return items;
    }

    public List<Item> showAll(String username) {
        List<Item> items = itemdao.findByUsername(username);
        return items;
    }

    @Override
    public List<Item> findByUsernameAndDetail(String username, String detail) {
        List<Item> items = itemdao.findByUsernameAndDetail(username, detail);
        return items;
    }
}
