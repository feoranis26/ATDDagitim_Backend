import unittest
import requests

class unittestTest(unittest.TestCase):
    def test_backHome(self):
        response = requests.get('https://api.sehirbahceleri.com.tr')
        self.assertEqual(response.status_code, 200, "Backend unreachable")
    def test_backProducts(self):
        response = requests.get('https://api.sehirbahceleri.com.tr/products')
        self.assertEqual(response.status_code, 200, "Products unreachable")
    def test_backSeeds(self):
        response = requests.get('https://api.sehirbahceleri.com.tr/seeds')
        self.assertEqual(response.status_code, 200, "Seeds unreachable")
    

if __name__ == '__main__':
    unittest.main()