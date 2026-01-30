import re
import requests
from bs4 import BeautifulSoup

def _to_html_url(doc_url: str) -> str:
    doc_url = doc_url.strip()

    base = doc_url.split("?")[0]
    return base + "?output=html"

def _extract_triplets_from_html(html: str):
    """
    Returns list of (x:int, char:str, y:int).
    Assumes first table contains header row: x-coordinate | Character | y-coordinate
    """
    soup = BeautifulSoup(html, "html.parser")
    table = soup.find("table")
    rows = table.find_all("tr")
    triplets = []

    for r in rows[1:]:  # skip header
        cells = [c.get_text(strip=True) for c in r.find_all(["td", "th"])]

        x_s, ch, y_s = cells[0], cells[1], cells[2]
        x_s = re.sub(r"\s+", "", x_s)
        y_s = re.sub(r"\s+", "", y_s)

        x = int(x_s)
        y = int(y_s)
        triplets.append((x, ch, y))

    return triplets


def decode_secret_message(doc_url: str) -> None:
    html_url = _to_html_url(doc_url)

    resp = requests.get(html_url, timeout=30)
    resp.raise_for_status()

    triplets = _extract_triplets_from_html(resp.text)
    if not triplets:
        raise ValueError("Parsed 0 coordinate rows from the document.")

    points = {}
    max_x = 0
    max_y = 0

    for x, ch, y in triplets:
        points[(x, y)] = ch
        max_x = max(max_x, x)
        max_y = max(max_y, y)

    grid = [[" "] * (max_x + 1) for _ in range(max_y + 1)]
    for (x, y), ch in points.items():
        grid[y][x] = ch

    for row in grid:
        print("".join(row))

decode_secret_message("https://docs.google.com/document/d/e/2PACX-1vRPzbNQcx5UriHSbZ-9vmsTow_R6RRe7eyAU60xIF9Dlz-vaHiHNO2TKgDi7jy4ZpTpNqM7EvEcfr_p/pub")
