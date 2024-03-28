import unittest
import requests

class frontend_test(unittest.TestCase):
    def test_addition(self):
        self.assertEqual(1 + 1, 2)
    def test_frontHome_test(self):
        response = requests.get('https://sehirbahceleri.com.tr')
        self.assertEqual(response.status_code, 200, "Frontend Home Page error")
    def test_frontProducts(self):
        response = requests.get('https://sehirbahceleri.com.tr/products')
        self.assertEqual(response.status_code, 200, "Frontend Products Page error")
    def test_frontSeeds(self):
        response = requests.get('https://sehirbahceleri.com.tr/seeds')
        self.assertEqual(response.status_code, 200, "Frontend Seeds Page error")