﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class User
{
    #region Attributes/Private Variables
    private int id;
    private string username;
    private string email;
    private string password;   
    private DateTime created_date;
    private string address;
    private string city;
    private string state;
    private string zipcode;
    private string first_name;
    private string last_name;
    private Account account;
    private bool hasAccount;
    private List<CreditCard> cards;
    private List<UserReview> reviews;
    #endregion
    public User(string username, string password, string email, string address, string city, string state, string zipcode, string first_name, string last_name)
	{
        this.username = username;
        this.password = password;
        this.email = email;
        this.address = address;
        this.city = city;
        this.state = state;
        this.zipcode = zipcode;
        this.first_name = first_name;
        this.last_name = last_name;

        //How is the account id determined?
        int account_id = 0;
        this.account = new Account(account_id, this.id);
        this.cards = new List<CreditCard>();
        this.reviews = new List<UserReview>();
	}
    public User(string username, string password, string email)
    {
        this.Username = username;
        this.Password = password;
        this.Email = email;
    }

    public void createAccount()
    {
        //How is the account id determined?
        int account_id = 0;
        this.account = new Account(account_id, this.id);
    }

    public void AddCard(string name, string number, DateTime expiry_date)
    {
        //How is the card id determined?
        int card_id = 0;
        CreditCard newCard = new CreditCard(card_id, this.id, number, name, expiry_date);
        this.addCard(newCard);
    }

    private void addCard(CreditCard card)
    {
        this.cards.Add(card);
    }

    public void bid(double amount, Auction auction)
    {
        //Check if bid amount is more than the top bid, and user has available funds
        if ((amount > auction.Top_bid) && (amount <= this.account.Available_balance))
        {
            auction.bid(amount, this);
            this.account.Available_balance -= amount;
        }
    }

    public void buyout(Auction auction)
    {
        if (auction.Buyout_price <= this.account.Available_balance)
        {
            auction.bid(auction.Buyout_price, this);
            account.Available_balance -= auction.Buyout_price;
            account.withdraw(auction.Buyout_price);
        }
    }

    public void createAuction(double min_bid, double buyout_price, DateTime end_time, string description, string img_url)
    {
        //How are these determined, and what's the difference between 'id' and 'item_id'?
        //Setting to 0 for now
        int id = 0;
        int item_id = 0;

        Auction newAuction = new Auction(id, this, item_id, min_bid, buyout_price, end_time, description, img_url);

        // Magic goes here
    }

    public void addReview(User submitter, string review_text, int rating)
    {
        //How is review_id determined?
        int review_id = 0;
        UserReview newReview = new UserReview(review_id, submitter.Id, this.Id, review_text, DateTime.Now, rating);
        reviews.Add(newReview);
    }

    public void writeReview(User user, string review_text, int rating)
    {
        //Can't write a review of yourself, dummy
        if (!user.Equals(this))
        {
            user.addReview(this, review_text, rating);
        }
    }
    //this doesn't belong here
    //public void Create_User(User user)
    //{
    //    MySQLClient add_user = new MySQLClient("173.194.241.131","auction_powers", "root", "root");
    //    string value = user.Username + ", " + user.Email + ", " + user.Password + ", " + user.Address + ", " + user.City + ", " + user.State + ", " + user.Zipcode + ", " + user.First_name + ", " + user.Last_name;
    //    add_user.Insert("user", "first_name, last_name, username, password, email, phone, address, city, state, zipcode", value);
    //}

    #region Properties
    public int Id
    {
        get { return id; }
        set { id = value; } // should never really be used
    }
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    public string Last_name
    {
        get { return last_name; }
        set { last_name = value; }
    }
    public string First_name
    {
        get { return first_name; }
        set { first_name = value; }
    }
    public string Zipcode
    {
        get { return zipcode; }
        set { zipcode = value; }
    }
    public string State
    {
        get { return state; }
        set { state = value; }
    }
    public string City
    {
        get { return city; }
        set { city = value; }
    }
    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    public DateTime Created_date
    {
        get { return created_date; }
        set { created_date = value; }//should never really be used
    }

    public List<UserReview> Reviews
    {
        get
        {
            return reviews;
        }
    }
    #endregion

}