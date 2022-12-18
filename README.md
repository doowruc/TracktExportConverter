# Trackt Export Converter

## Introduction

In December 2022 the Trakt service had a database crash which has resulted in data loss

On 17th December, Trakt released a beta version of the iOS App via Apple's TestFlight

This beta version has an export function which dumps the local data to JSON files

This may or may not be importable at a later date

See forum post for more details: https://forums.trakt.tv/t/export-specific-version-ios-1-8-29/12691

This web application imports the `<username>_watched_shows.json` and `<username>_watched_movies.json` files and displays them in a more user-friendly watch history list, along with options to export to Excel or CSV, or copy to clipboard

## Usage

The site is available at https://traktexportconverter.doowruc.dev

Click on Watched

Click on Import then Shows or Movies

Browse to and select the relevant JSON file

The page loads the converted data

Optionally, click on Export then Excel, CSV, or Copy to clipboard

## Security / Privacy

The site does not store your JSON data, it uploads it to the server's memory in order to perform the conversion and return the converted data back to your browser for display

## Statement from Trakt (As of 18/12/2022)

### Trakt Data Recovery
On December 11 at 7:30 pm PST our main database crashed and corrupted some of the data. We're deeply sorry for the extended downtime and we'll do better moving forward. Updates to our automated backups are already in place and they will be tested on an ongoing basis.
- Data prior to November 7 is fully restored.
- Watched history after November 7 is still being recovered.
  - The data should be available next week.
  - You'll be able to import any history we recover.
- All other data after November 7 has been imported.
- Some data might be permanently lost due to data corruption.
- Trakt API will be turned on next week.
- Active VIP members will get 2 free months of membership.


