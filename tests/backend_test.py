import unittest
import requests

class unittestTest(unittest.TestCase):
    def test_backHome(self):
        response = requests.get('https://api.sehirbahceleri.com.tr')
        self.assertEqual(response.status_code, 200, "Backend unreachable")
    def test_backProducts(self):
        response = requests.get('https://api.sehirbahceleri.com.tr/products?Page=1&PageSize=10')
        self.assertEqual(response.status_code, 200, "Products unreachable")
    def test_backCategories(self):
        response = requests.get('https://api.sehirbahceleri.com.tr/Category/all')
        self.assertEqual(response.status_code, 200, "Seeds unreachable")
    

if __name__ == '__main__':
    unittest.main()