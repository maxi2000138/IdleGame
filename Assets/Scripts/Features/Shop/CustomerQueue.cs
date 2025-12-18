using System.Collections.Generic;
using Features.Units.Customer;
using UnityEngine;

namespace Features.Shop
{
  public class CustomerQueue : MonoBehaviour
  {
    private LinkedList<Customer> _customers = new LinkedList<Customer>();

    public List<Transform> CustomersPositions;
    public Transform FinalPoint;

    public int Amount => _customers.Count;

    public void EnqueueCustomer(Customer customer)
    {
      _customers.AddLast(customer);
    }

    public Customer DequeueCustomer()
    {
      if (_customers.Count == 0) return null;

      var customer = _customers.First.Value;
      _customers.RemoveFirst();

      return customer;
    }

    public IEnumerator<Customer> GetEnumerator() => _customers.GetEnumerator();
  }
}