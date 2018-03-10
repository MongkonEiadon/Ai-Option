import * as React from 'react';
import * as LoginStore from '../store/Login';
import * as ReactDom from 'react-dom'
import { Grid, Col, Row, Button, Checkbox, Form, FormGroup, FormControl, InputGroup, InputGroupAddon } from 'react-bootstrap';

import { Link, RouteComponentProps } from 'react-router-dom';
import { ApplicationState } from '../store';
import { connect } from 'react-redux';

type LoginProps =
    LoginStore.AuthenticateActionType & typeof LoginStore.actionCreators & RouteComponentProps<{}>;


class Login extends React.Component<LoginProps,{}>{

    constructor(props: LoginProps){
        super(props);
        this.state = {
            email: '',
            password: ''
        }
    }

    handleChange = (e: any) =>{
        //this.state({ [e.target.name]: e.target.name});
    }

    login = (event: React.FormEvent<Form>) => {
        //this.props.startLogin(this.state);
        event.preventDefault();
    }


    public render() {
        return <Grid className="omb_login">
        <h3 className="omb_authTitle">Login or <Link to={'/Register'}>Register as new user</Link></h3>
        <Row className="omb_socialButtons">
            <Col xs={4} sm={2} smOffset={3} >
                <Link to="#" className="btn btn-lg btn-block omb_btn-facebook">
                    <i className="fa fa-facebook visible-xs"></i>
                    <span className="hidden-xs">Facebook</span>
                </Link>
            </Col>
            <Col xs={4} sm={2} >
                <Link to="#" className="btn btn-lg btn-block omb_btn-twitter">
                    <i className="fa fa-twitter visible-xs"></i>
                    <span className="hidden-xs">Twitter</span>
                </Link>
            </Col>
            <Col xs={4} sm={2} >
                <Link to="#" className="btn btn-lg btn-block omb_btn-google">
                    <i className="fa fa-google-plus visible-xs"></i>
                    <span className="hidden-xs">Google+</span>
                </Link>
            </Col>
        </Row>

        <Row className="omb_loginOr">
            <Col xs={12} sm={6} smOffset={3} >
                <hr className="omb_hrOr" />
                <span className="omb_spanOr">or</span>
            </Col>
        </Row>

        <Row>
            <Col xs={12} sm={6} smOffset={3} >
                <Form className="omb_loginForm" onSubmit={this.login} autoComplete="off">
                    <FormGroup >
                        <InputGroup>
                            <InputGroup.Addon><i className="fa fa-user" /></InputGroup.Addon>
                            <FormControl name="email" type="text" onChange={this.handleChange} placeholder="Email adres" />
                        </InputGroup>
                        <FormControl.Feedback />
                    </FormGroup>

                    <FormGroup>
                        <InputGroup>
                            <InputGroup.Addon><i className="fa fa-lock" /></InputGroup.Addon>
                            <FormControl name="password" type="password" onChange={this.handleChange} placeholder="Password" />
                        </InputGroup>
                        <FormControl.Feedback />
                    </FormGroup>

                    <Button className="btn btn-lg btn-primary btn-block" type="submit">Inloggen</Button>
                </Form>
            </Col>
        </Row>

        <Row>
            <Col xs={12} sm={3} smOffset={3}>
                <FormGroup>
                    <Checkbox>Keep me logged in</Checkbox>
                </FormGroup>
            </Col>
            <Col xs={12} sm={3}>
                <p className="omb_forgotPwd">
                    <Link to="#">Password vergeten?</Link>
                </p>
            </Col>
        </Row>	    
    </Grid>
    }
}

export default connect(
    (state: ApplicationState) => state.login,
    LoginStore.actionCreators
)(Login)
