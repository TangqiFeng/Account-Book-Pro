# Account-Book-Pro

> author: [Tangqi Feng](https://tangqifeng.github.io/)

Feature demo video: [link](https://www.youtube.com/watch?v=H9LLYazQMS0)

Year 4, B.Sc. (Hons) in Software Development --- [Mobile Applications development（ UWP ](https://github.com/TangqiFeng/Account-Book-Pro/wiki)

> Module: Mobile Applications development 2 / 4th Year  
> Lecturer: Dr Damien Costello

This application helps user to note daily expenses, in order to provide a good way to organize their wealth.

## How this repository organized

This repository includes two parts:
* Microsoft UWP project. - the front-end project write in c#
  * this app is using **MVVM** design model
  * this app combines GPS function getting user loc
  * this app allow user to login/refister, add/delete/search items by sending http request to interact with server

* springboot projecct, - the back-end server write in Java
  * this server provides some API for responsing requests from client
  * this server connect to MongoDB
  
#### For more details, see the design.doc
