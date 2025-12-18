using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Features.Units.Customer;
using Infrastructure.Factory;
using UnityEngine;

namespace Features.Shop
{
  public class Shop : MonoBehaviour
  {
    [field: SerializeField] public CustomerQueue CustomerQueue { get; private set; }
    [field: SerializeField] public Transform SellPoint { get; private set; }

    private IGameFactory _gameFactory;
    private UniTaskCompletionSource _updateCustomersPositions;
    private Shop _shop;
    public bool HasCustomers => CustomerQueue.Amount > 0;

    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }

    public void SpawnCustomers()
    {
      foreach (var customersPosition in CustomerQueue.CustomersPositions)
      {
        var customer = _gameFactory.CreateCustomer(customersPosition.position, CustomerQueue.transform);
        CustomerQueue.EnqueueCustomer(customer);
      }
    }

    public async UniTask<Customer> WaitForCustomer()
    {
      if (_updateCustomersPositions != null)
        await _updateCustomersPositions.Task;

      return CustomerQueue.DequeueCustomer();
    }

    public async UniTask UpdateAllCustomersPosition()
    {
      _updateCustomersPositions = new UniTaskCompletionSource();
      var newCustomer = _gameFactory.CreateCustomer(CustomerQueue.CustomersPositions.Last().position, CustomerQueue.transform.parent);
      CustomerQueue.EnqueueCustomer(newCustomer);

      var moveToPositions = new List<UniTask>();

      var index = 0;
      foreach (var customer in CustomerQueue)
      {
        moveToPositions.Add(UpdateCustomerPosition(CustomerQueue, customer, index));
        index++;
      }

      await UniTask.WhenAll(moveToPositions);
      _updateCustomersPositions.TrySetResult();
    }

    private async UniTask UpdateCustomerPosition(CustomerQueue customerQueue, Customer customer, int index)
    {
      customer.Agent.SetDestination(customerQueue.CustomersPositions[index].position);
      customer.CustomerAnimator.Run();
      await UniTask.WaitWhile(() => customer.Agent.hasPath);
      customer.CustomerAnimator.Idle();
      customer.CustomerMover.RotateForward();
    }
  }
}