import * as React from "react";
const { hubConnection } = require('signalr-shimmy');

export interface HelloProps { compiler: string; framework: string; }
export interface HelloState { webapiMessage: string, singalrMessage: string }

// 'HelloProps' describes the shape of props.
// State is never set so we use the '{}' type.
export class Hello extends React.Component<HelloProps, HelloState> {

    constructor(props: HelloProps) {
        super(props);
        this.state = {
            singalrMessage: '',
            webapiMessage: ''
        };
    }

    private onConnectToSignalR = () => {
        const endpoint = 'http://localhost:7654'
        const newConnection = hubConnection(endpoint, { logging: true });
        const newHub = newConnection.createHubProxy('HelloWorldHub');
        newHub.on('broadcastMessage', (myMessage: string) => {
            this.setState({
                singalrMessage: myMessage
            })
        });
        newConnection.start().done(function () { console.log('Now connected, connection ID=' + newConnection.id); })
            .fail(function (error: any) { console.log('Could not connect' + error); });;
    };

    private onWebApiClick = () => {
        fetch('http://localhost:7654/api/helloworld/get').then(response => {
            return response.text();
        }).then(message => {
            this.setState({ webapiMessage: message });
        });
    }
    render() {
        return (
            <div>
                <button onClick={this.onWebApiClick}>Go to WebAPI</button><span>{this.state.webapiMessage}</span>
                <button onClick={this.onConnectToSignalR}>Connect to SignalR</button><span>{this.state.singalrMessage}</span>
            </div>
        )

    }
}