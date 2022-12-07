const routes=[
    {path:'/contact',component:contact}]

const router = VueRouter.createRouter({
    history: VueRouter.createWebHashHistory(),
    routes
 })
 
 const app = Vue.createApp({})
app.use(router)
app.mount('#app')