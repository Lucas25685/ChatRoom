import { HubConnection, HubConnectionBuilder, LogLevel, HubConnectionState } from '@microsoft/signalr';
import { Subject, Observable } from 'rxjs';

export class SignalRClientBase {
	private readonly _onReconnectedSubject = new Subject<void>();

	/**
	 * Observable to listen to reconnection events
	 */
	public readonly onReconnected$: Observable<void> = this._onReconnectedSubject.asObservable();

	protected hubUrl: string = '';

	protected readonly getConnectionPromise: Promise<void>;

	protected readonly _hubConnection: HubConnection;

	constructor(hubUrl: string) {
		this.hubUrl = hubUrl;

		// 1. create the hub connection
		this._hubConnection = new HubConnectionBuilder()
			.withUrl(hubUrl)
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build();

		// 2. start the connection
		this.getConnectionPromise = this.startConnection();

		// 3. handle connection events
		this._hubConnection.onreconnecting(msg => {
			console.log('Reconnecting...', { message: msg });
		});

		this._hubConnection.onreconnected(connectionId => {
			console.log('Reconnected. Connection ID:', connectionId, 'State:', this._hubConnection.state);

			this._onReconnectedSubject.next();
		});

		this._hubConnection.onclose(error => {
			console.log('Connection closed', { error });
		});
	}

	/**
	 * Start the conneccion to the messaging hub, to recieve new messages
	 */
	protected async startConnection() {
		if (this._hubConnection.state !== HubConnectionState.Connected) {
			try {
				console.log(`Attempting to connect to the messaging hub ${this.hubUrl}...`);

				await this._hubConnection.start();

				console.log(`Connected to the messaging hub ${this.hubUrl} State: ${this._hubConnection.state}`);
			} catch (err) {
				console.log('Error while establishing connection :(', err);

				setTimeout(() => this.startConnection(), 5000);
			}
		}
	}
}
