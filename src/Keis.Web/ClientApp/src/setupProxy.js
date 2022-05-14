const createProxyMiddleware = require("http-proxy-middleware");
const { env } = require("process");

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
  ? env.ASPNETCORE_URLS.split(";")[0]
  : "http://localhost:58729";

const context = [
  "/methods/auth.login",
  "/methods/dashboard.count",

  "/methods/ticket.list",
  "/methods/ticket.get",
  "/methods/ticket.create",

  "/methods/ticket.postcomment",
  
  "/methods/ticket.status",
  "/methods/ticket.assign",
  "/methods/ticket.department",
  "/methods/ticket.team",
  "/methods/ticket.sla",
  "/methods/ticket.duedate",
  "/methods/ticket.tickettype",
  "/methods/ticket.helptopic",

  "/methods/agent.list",
  "/methods/agent.get",
  "/methods/agent.create",

  "/methods/businesshour.list",
  "/methods/businesshour.get",
  "/methods/businesshour.create",

  "/methods/department.list",
  "/methods/department.get",
  "/methods/department.create",

  "/methods/team.list",
  "/methods/team.get",
  "/methods/team.create",

  "/methods/tag.list",
  "/methods/tag.get",
  "/methods/tag.create",

  "/methods/tickettype.list",
  "/methods/tickettype.get",
  "/methods/tickettype.create",

  "/methods/helptopic.list",
  "/methods/helptopic.get",
  "/methods/helptopic.create",

  "/methods/sla.list",
  "/methods/sla.get",
  "/methods/sla.create",

  "/methods/workflow.list",
  "/methods/workflow.get",
  "/methods/workflow.create",
  "/methods/workflow.event",
  "/methods/workflow.criteria",
  "/methods/workflow.action",
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: "Keep-Alive",
    },
  });

  app.use(appProxy);
};
