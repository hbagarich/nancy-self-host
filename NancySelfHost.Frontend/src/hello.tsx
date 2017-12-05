import * as React from "react";
const { hubConnection } = require('signalr-shimmy');

export interface HelloProps { compiler: string; framework: string; }

// 'HelloProps' describes the shape of props.
// State is never set so we use the '{}' type.
export class Hello extends React.Component<HelloProps, {}> {

    constructor(props: HelloProps) {
        super(props);
        const endpoint = 'http://localhost:7654'
        const newConnection = hubConnection(endpoint, { logging: true });
        const newHub = newConnection.createHubProxy('HelloWorldHub');
        newHub.on('broadcastMessage', (myMessage: string) => {
            console.log(myMessage);
        });
        newConnection.start().done(function(){ console.log('Now connected, connection ID=' + newConnection.id); })
        .fail(function(error:any){ console.log('Could not connect' + error); });;
    }
    private onWebApiClick() {
        fetch('http://localhost:7654/api/helloworld/get').then(function (response) {
            return response.text();
        }).then(function (message) {
            console.log(message);
        });
    }
    render() {
        return <h1 onClick={this.onWebApiClick}>Hello from {this.props.compiler} and {this.props.framework}!</h1>;
    }
}