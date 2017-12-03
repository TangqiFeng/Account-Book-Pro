package com.kyle.demo.models;

import org.springframework.data.annotation.Id;

public class Item {

    @Id
    private String id;
    private String username;
    private String detail;
    private String operate;
    private String value;
    private String currency;
    private String location;
    private String date;

    public Item(String username, String detail, String operate, String value,
                String currency, String location, String date) {
        this.username = username;
        this.detail = detail;
        this.operate = operate;
        this.value = value;
        this.currency = currency;
        this.location = location;
        this.date = date;
    }

    public Item() {
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getDetail() {
        return detail;
    }

    public void setDetail(String detail) {
        this.detail = detail;
    }

    public String getOperate() {
        return operate;
    }

    public void setOperate(String operate) {
        this.operate = operate;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public String getCurrency() {
        return currency;
    }

    public void setCurrency(String currency) {
        this.currency = currency;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }
}
