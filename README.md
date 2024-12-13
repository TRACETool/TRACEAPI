# TRACEAPI



### Description
An API that facilitates immutable logging, also receives webhooks from various version control system, and creates and manages database through code-first approach using MS EFCore. This solution also acts as a data source for an anomaly detection model used for detecting anomaly in repo activities on VCSs.

### Key Features
- Immutable Logging.
- Webhook handling.
- Real-time alerts.

### Dependencies
This project relies on the following:
1. **[BackgroundService](https://github.com/TRACETool/BackgroundService)**
   - Background service to handle asynchronous tasks.
2. **[Anomaly detection module](https://github.com/TRACETool/AnomalyModule)**
   - Detect anomalies in repo activities.
### Optional
**[TRACEWeb](https://github.com/TRACETool/TRACEWeb)**
   - A web interface for user to view reports. You can build your own web interface if required.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/TRACETook/TRACEAPI.git

### Contribution
 Contributors are welcome to contribute to building an open source supply chain security software with best interests in mind.

 
